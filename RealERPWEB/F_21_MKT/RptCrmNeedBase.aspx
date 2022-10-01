<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptCrmNeedBase.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptCrmNeedBase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <style>
        .tblborder {
            border: none;
        }

            .tblborder td {
                border: none;
            }

        .visibleshow .grvHeader, .grvFooter {
            display: none;
        }
    </style>

    <script>
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            onclicksortbtn();

        });

        function pageLoaded() {



            try {

                var gvSummary = $('#<%=this.gvSummary.ClientID %>');
                gvSummary.Scrollable();
            }
            catch (e) {
                alert(e.message);
            }
        }

        function onchangetrigger() {
            onclicksortbtn();
            inputekeyup();
        }

        function onclicksortbtn() {
            var properties = [
                'sl',
                'pid',
                'gen',
                'prospectdet',
                'prof',
                'assoc',
                'head',
                'status',
                'typ'
            ];

            $.each(properties, function (i, val) {
                var orderClass = '';
                $("#" + val).click(function (e) {
                    e.preventDefault();
                    var index = $(this).index(".indexing");
                    sortTable(index);
                });
            });

        }


        function sortTable(n) {
            var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
            table = document.getElementById("<%=gvSummary.ClientID %>");
            switching = true;
            // Set the sorting direction to ascending:
            dir = "asc";
            /* Make a loop that will continue until
            no switching has been done: */
            while (switching) {
                // Start by saying: no switching is done:
                switching = false;
                rows = table.rows;
                /* Loop through all table rows (except the
                first, which contains table headers): */
                for (i = 1; i < (table.rows.length - 2); i++) {
                    // Start by saying there should be no switching:
                    shouldSwitch = false;
                    /* Get the two elements you want to compare,
                    one from current row and one from the next: */
                    x = rows[i].getElementsByTagName("TD")[n];
                    y = rows[i + 1].getElementsByTagName("TD")[n];
                    //console.log(x.textContent.toLowerCase(), y.textContent.toLowerCase());
                    /* Check if the two rows should switch place,
                    based on the direction, asc or desc: */
                    if (dir == "asc") {
                        if (x.textContent.toLowerCase() > y.textContent.toLowerCase()) {
                            // If so, mark as a switch and break the loop:
                            shouldSwitch = true;
                            break;
                        }
                    } else if (dir == "desc") {
                        if (x.textContent.toLowerCase() < y.textContent.toLowerCase()) {
                            // If so, mark as a switch and break the loop:
                            shouldSwitch = true;
                            break;
                        }
                    }
                }
                if (shouldSwitch) {
                    /* If a switch has been marked, make the switch
                    and mark that a switch has been done: */
                    rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                    switching = true;
                    // Each time a switch is done, increase this count by 1:
                    switchcount++;
                } else {
                    /* If no switching has been done AND the direction is "asc",
                    set the direction to "desc" and run the while loop again. */
                    if (switchcount == 0 && dir == "asc") {
                        dir = "desc";
                        switching = true;
                    }
                }
            }
        }


        function inputekeyup() {
            var value = $("#myInput").val().toLowerCase();
            $("#<%=gvSummary.ClientID %> tr:not(:first)").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });

        }

        function Search_Gridview(strKey, cellNr) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvSummary.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {

                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;

            }
        }




        function OpenGvModal() {

            $('#GridHeader').modal('toggle');
        }

        function CloseGvModal() {

            $('#GridHeader').modal('hide');
        }
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid container-data">
                <div class="card-body" style="min-height: 600px;">
                    <div class="row mt-2">
                        <%--<div class="col-md-1">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                </div>--%>
                        <div class="col-md-3">
                            <input type="text" id="myInput" onkeyup="inputekeyup();" placeholder="Search.." title="Type" class="form-control">
                        </div>
                        <div clsss="col-md-2">

                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-sm" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                            </asp:HyperLink>

                        </div>
                    </div>
                    <div class="row">

                        <div class="table table-responsive">

                            <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea" data-toggle="table"
                                data-search="true"
                                data-show-columns="true">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>

                                            <asp:LinkButton ID="lnkgvHeader" runat="server" Font-Bold="True" CssClass="indexing" Height="16px" ToolTip="Edit Header" OnClick="lnkgvHeader_Click"><i class="fa fa-th-large" aria-hidden="true"></i></asp:LinkButton>
                                            <%--                                          <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                            </asp:HyperLink>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="serialno" runat="server" Style="text-align: left" CssClass="table-data"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lsircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>

                                            <asp:Label ID="ldesig" runat="server" Width="40px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P-ID">
                                        <HeaderTemplate>
                                            <asp:Label ID="txtsrcA" runat="server" Width="60px">P-ID</asp:Label>
                                            <%--<asp:TextBox ID="txtsrcA" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="P-ID" onkeyup="Search_Gridview(this,1)" Font-Size="12px"></asp:TextBox>--%>
                                            <a id="pid" class="filter__link filter__link--number indexing" href="#"><i class="fa fa-sort" aria-hidden="true" onclick="onclicksortbtn()"></i></a>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lsircode1" runat="server" Width="80px" Font-Size="12px" CssClass="table-data"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Generated">
                                        <HeaderTemplate>
                                            <asp:Label ID="txtsrcB" runat="server" Width="60px">Generated</asp:Label>
                                            <%--<asp:TextBox ID="txtsrcB" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Generated" onkeyup="Search_Gridview(this,2)" Font-Size="12px"></asp:TextBox>--%>
                                            <a id="gen" class="filter__link filter__link--number indexing" href="#"><i class="fa fa-sort" aria-hidden="true" onclick="onclicksortbtn()"></i></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgenerated" runat="server" Width="80px" Font-Size="12px" CssClass="table-data"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prospect Details">
                                        <HeaderTemplate>
                                            <asp:Label ID="txtsrcC" runat="server" Width="130px">Prospect Details</asp:Label>
                                            <%--<asp:TextBox ID="txtsrcC" BackColor="Transparent" BorderStyle="None" runat="server" Width="130px" placeholder="Prospect Details" onkeyup="Search_Gridview(this,3)" Font-Size="12px"></asp:TextBox>--%>
                                            <a id="prospectdet" class="filter__link filter__link--number indexing" href="#"><i class="fa fa-sort" aria-hidden="true" onclick="onclicksortbtn()"></i></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="ldesc" runat="server" Width="150px" Font-Size="12px" CssClass="table-data"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profession">
                                        <HeaderTemplate>
                                            <asp:Label ID="txtsrcG" runat="server" Width="80px">Profession</asp:Label>
                                            <%--<asp:TextBox ID="txtsrcG" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Profession" onkeyup="Search_Gridview(this,4)" Font-Size="12px"></asp:TextBox>--%>
                                            <a id="prof" class="filter__link filter__link--number indexing" href="#"><i class="fa fa-sort" aria-hidden="true" onclick="onclicksortbtn()"></i></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lprof" runat="server" Width="100px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Associate">
                                        <HeaderTemplate>
                                            <asp:Label ID="txtsrcD" runat="server" Width="100px">Associate</asp:Label>
                                            <%--<asp:TextBox ID="txtsrcD" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Associate" onkeyup="Search_Gridview(this,5)" Font-Size="12px"></asp:TextBox>--%>
                                            <a id="assoc" class="filter__link filter__link--number indexing" href="#"><i class="fa fa-sort" aria-hidden="true" onclick="onclicksortbtn()"></i></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lassoc" runat="server" Width="120px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Team Head">
                                        <HeaderTemplate>
                                            <asp:Label ID="txtsrcE" runat="server" Width="100px">Team Head</asp:Label>

                                            <a id="head" class="filter__link filter__link--number indexing" href="#"><i class="fa fa-sort" aria-hidden="true" onclick="onclicksortbtn()"></i></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblbusername" runat="server" Width="120px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Status">
                                        <HeaderTemplate>
                                            <asp:Label ID="txtsrcps" runat="server" Width="60px">Previous Status</asp:Label>

                                            <a id="psstatus" class="filter__link filter__link--number indexing" href="#"><i class="fa fa-sort" aria-hidden="true" onclick="onclicksortbtn()"></i></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbllpsstatus" runat="server" Width="80px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preleadst")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                            <asp:Label ID="txtsrcF" runat="server" Width="60px">Status</asp:Label>
                                            <%--<asp:TextBox ID="txtsrcF" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Status" onkeyup="Search_Gridview(this,7)" Font-Size="12px"></asp:TextBox>--%>
                                            <a id="status" class="filter__link filter__link--number indexing" href="#"><i class="fa fa-sort" aria-hidden="true" onclick="onclicksortbtn()"></i></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbllstatus" runat="server" Width="80px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                            <asp:Label ID="txtsrcH" runat="server" Width="40px">Type</asp:Label>
                                            <a id="typ" class="filter__link filter__link--number indexing" href="#"><i class="fa fa-sort" aria-hidden="true" onclick="onclicksortbtn()" font-size="12px"></i></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="llTyp" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadType")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sold On  <br> Date/Win Date" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllnfollowupdateSold" runat="server" Width="100px" Font-Size="12px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "solddate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "solddate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Last Follow <br> Up date" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lastlbllnfollowupdate" runat="server" Width="100px" Font-Size="12px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ldiscussdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ldiscussdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Next Follow <br> Up date" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllnfollowupdate" runat="server" Width="100px" Font-Size="12px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Lead Source" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lLSrc" runat="server" Width="100px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mobile" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" runat="server" Width="80px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <%--This is Initial start Project (pactdesc) --%>

                                    <asp:TemplateField HeaderText="Interest Project" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIntproject" runat="server" Width="120px" Font-Size="12px" 
                                                class='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sameprjclass")) %>'                                                
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--This is Main Last Lead Project--%>
                                    <asp:TemplateField HeaderText="Project" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblproject" runat="server" Width="120px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstprjdiscussion")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               

                                    <asp:TemplateField HeaderText="Approve Date" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lappdat" runat="server" Width="80px" Font-Size="12px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pref. Location" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lprefdesc" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="App. Size" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lAptsize" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aptsize")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="App. Type" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lapttyp" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apttyp")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="App. Floor" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppfl" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flordesc1")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Facing" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFacing" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facedesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblView" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "viewdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budget" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBudget" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apttyp")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budget desc" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInterest" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bgddesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Source Remarks" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsourcremarkst" runat="server" Width="100px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sourcremarks")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Lost On Date" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLostDate" runat="server" Width="100px" Font-Size="12px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lostdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : 
                                                    Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lostdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" Font-Size="12px" />
                            </asp:GridView>

                        </div>
                    </div>

                    <div class="modal fade" id="GridHeader" tabindex="-1" role="dialog" aria-labelledby="gridModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title" id="gridModalLabel">Select Grid Header</h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <h5>Available Fields</h5>

                                            <asp:GridView ID="gvCurrent" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkgv" runat="server" />
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lindex" runat="server" Font-Size="12px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lLSrc" runat="server" Font-Size="13px" CssClass="ml-3"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-6">
                                            <h5>Selected Fields</h5>

                                            <asp:GridView ID="gvPrev" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkgv" runat="server" Checked="true" />
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lindex" runat="server" Font-Size="12px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lLSrc" runat="server" Font-Size="13px" CssClass="ml-3"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>


                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    <asp:LinkButton ID="lnkgvListShow" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClientClick="CloseGvModal();" OnClick="lnkgvListShow_Click">Update List</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>


