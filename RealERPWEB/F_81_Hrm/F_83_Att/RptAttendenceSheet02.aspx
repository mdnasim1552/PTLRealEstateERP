<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptAttendenceSheet02.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.RptAttendenceSheet02" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        $(document).ready(function ()
        {
            $(".select2").select2();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded()
        {
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
   

          


            $('.select2').each(function ()
            {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });         
        }
      <%--  function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvMonthlyattSummary.ClientID %>");
             var rowData;
             for (var i = 1; i < tblData.rows.length; i++) {

                 rowData = tblData.rows[i].innerHTML;
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
        }--%>



    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <div class="card card-fluid mt-5" style="min-height: 550px;">
                <div class="card-header">
                  
                    <div class="row">
                        <asp:Label ID="lbl" runat="server" CssClass="col-1 col-form-label">Company</asp:Label>
                        <div class="col-3">

                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <asp:Label ID="Label1" runat="server" CssClass="col-1 col-form-label">Department</asp:Label>
                        <div class="col-3">

                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <asp:Label ID="lblSection" runat="server" CssClass="col-1 col-form-label text-right">Section</asp:Label>
                        <div class="col-3" id="PnlSection" runat="server">
                            <asp:DropDownList ID="ddlgroup" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            <%--<asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control select2" SelectionMode="Multiple" ></asp:ListBox>--%>
                        </div>

                        <asp:Label ID="lblfrmdate" runat="server" CssClass="col-1 col-form-label">From</asp:Label>

                        <div class="col-2">
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                            <cc1:calendarextender id="txtfromdate_CalendarExtender" runat="server" format="dd-MMM-yyyy" targetcontrolid="txtfromdate"></cc1:calendarextender>
                        </div>
                        <asp:Label ID="lbltodate" runat="server" CssClass="col-1 col-form-label">To</asp:Label>


                        <div class="col-2">

                            <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                            <cc1:calendarextender id="txttodate_CalendarExtender" runat="server" format="dd-MMM-yyyy" targetcontrolid="txttodate"></cc1:calendarextender>

                        </div>

                        <div class="col-2" hidden="hidden">
                            <asp:RadioButtonList ID="rbtnAttStatus" runat="server" AutoPostBack="True"
                                CssClass="custom-control custom-control-inline custom-checkbox"
                                Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Time wise</asp:ListItem>
                                <asp:ListItem>Att Status</asp:ListItem>

                            </asp:RadioButtonList>

                        </div>

                        <div class="col-1">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Show</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div id="pnlDesig" runat="server" visible="false">
                            <div class="col-md-3">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                        <asp:Label ID="lblfrmDesig" runat="server" CssClass="btn btn-secondary btn-sm">Form</asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlfrmDesig" runat="server" OnSelectedIndexChanged="ddlfrmDesig_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm" Width="100px" TabIndex="6">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                        <asp:Label ID="lbltoDesig" runat="server" CssClass="btn btn-secondary btn-sm">To</asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlToDesig" runat="server" Width="120" CssClass="form-control form-control-sm" TabIndex="6">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                     <asp:Panel runat="server" ID="pnlmonthlyatt">

                         <div class="col-3">
                                  <div class="input-group input-group-alt">
                                                <div class="input-group-prepend ">
                                                    <asp:Label ID="Label3" runat="server" CssClass="btn btn-secondary btn-sm">Search</asp:Label>
                                                </div>
                                                <asp:TextBox ID="inputtextbox" Style="height: 29px" runat="server" CssClass="form-control" placeholder="Search here..." onkeyup="Search_Gridview(this)"></asp:TextBox>

                                            </div>
                                    </div>
                                <br />

                            <div class="table-responsive" id="DelaisAttinfo" runat="server">
                                <asp:GridView ID="gvMonthlyAtt" runat="server" PageSize="15"
                                    AutoGenerateColumns="False" ShowFooter="True" AllowPaging="True" OnPageIndexChanging="gvMonthlyAtt_PageIndexChanging"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCode" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno"))%>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvName" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdept" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdsig" runat="server"  Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvday" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "addday")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="01">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv01" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col1"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col1o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="02">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv02" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col2"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col2o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="03">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv03" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col3"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col3o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="04">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv04" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col4"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col4o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="05">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv05" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col5"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col5o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="06">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv06" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col6"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col6o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="07">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv07" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col7"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col7o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="08">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv08" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col8"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col8o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="09">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv09" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col9"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col9o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv10" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col10"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col10o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv11" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col11"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col11o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv12" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col12"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col12o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="13">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv13" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col13"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col13o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="14">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv14" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col14"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col14o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="15">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv15" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col15"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col15o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="16">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv16" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col16"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col16o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="17">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv17" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col17"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col17o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="18">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv18" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col18"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col18o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="19">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv19" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col19"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col19o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="20">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv20" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col20"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col20o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="21">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv21" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col21"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col21o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="22">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv22" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col22"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col22o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="23">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv23" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col23"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col23o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="24">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv24" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col24"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col24o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="25">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv25" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col25"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col25o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="26">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv26" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col26"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col26o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="27">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv27" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col27"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col27o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="28">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv28" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col28"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col12o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="29">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv29" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col29"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col29o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="30">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv30" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col30"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col30o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="31">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv31" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col31"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col31o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467"  />
                                </asp:GridView>
                            </div>

                            
                            <div class="table-responsive" id="SummaryAttinfo" runat="server">
                                
                                <asp:GridView ID="gvMonthlyattSummary" runat="server" AutoGenerateColumns="False" ShowFooter="True"  
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvNameSumm" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                                                              <HeaderTemplate>
                                                    <asp:Label ID="lblexl" runat="server" Font-Bold="True" Width="100px"
                                                        Text="Section">
                                                        <asp:HyperLink ID="hlbtntbCdataExelSP2" runat="server"
                                                            CssClass="btn btn-success ml-2 btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                    </asp:Label>
                                                </HeaderTemplate>

                                            <ItemStyle HorizontalAlign="Center" />

                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Id Card">
                                            <ItemTemplate>
                                                <asp:Label ID="lgidcardsumm" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="01" HeaderStyle-Wrap="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv01summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col1s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col1s"))=="LP"?"bg-danger d-block":""%>'
                                                    
                                                    
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col1s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="02">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv02summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col2s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col2s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col2s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="03">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv03summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col3s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col3s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col3s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="04">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv04summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col4s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col4s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col4s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="05">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv05summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col5s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col5s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col5s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="06">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv06summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col6s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col6s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col6s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="07">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv07summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col7s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col7s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col7s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="08">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv08summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col8s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col8s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col8s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="09">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv09summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col9s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col9s"))=="LP"?"bg-danger d-block":""%>' runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col9s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv10summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col10s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col10s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col10s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv11summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col11s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col11s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col11s"))
                                           %>'
                                                    Font-Size="11px" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv12summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col12s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col12s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col12s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="13">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv13summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col13s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col13s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col13s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="14">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv14summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col14s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col14s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col14s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="15">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv15summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col15s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col15s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col15s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="16">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv16summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col16s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col16s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col16s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="17">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv17summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col17s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col17s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col17s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="18">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv18summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col18s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col18s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col18s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="19">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv19summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col19s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col19s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col19s"))
                                         %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="20">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv20summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col20s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col20s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col20s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="21">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv21summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col21s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col21s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col21s"))
                                        %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="22">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv22summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col22s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col22s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col22s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="23">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv23summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col23s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col23s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col23s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="24">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv24summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col24s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col24s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col24s"))
                                          %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="25">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv25summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col25s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col25s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col25s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="26">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv26summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col26s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col26s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col26s"))
                                       %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="27">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv27summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col27s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col27s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col27s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="28">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv28summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col28s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col28s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col28s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="29">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv29summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col29s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col29s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col29s"))
                                          %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="30">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv30summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col30s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col30s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col30s"))
                                          %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="31">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv31summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col31s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col31s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col31s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Pesent" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPresent" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>

                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Late">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllate" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Late<br> Deduction" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllevded" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "levded")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="LP">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllateabs" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "abslate")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField> 
                                        
                                        <asp:TemplateField HeaderText="Absent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblabsent" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Holiday" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblholiday" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "holyday")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblleave" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tleav")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total <br> Deduction" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotal" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpayable")).ToString("#,##0.0; ")
                                             %>'
                                                    Font-Size="11px" ></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                            
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" />
                                </asp:GridView>

                                <fieldset class="scheduler-border fieldset_A" id="StatusReport" runat="server" visible="false">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class=" row col-md-11">
                                                <span style="font-size: 14px; color: blue" id="statusatt" runat="server">Present=P,  Absent =A,  Late=L,  Late Present=LP,  Casual Leave=CL, Sick Leave=SL, Earned Leave=EL, Without Pay Leave= WPL,  Weekend=W, Special Thursday=ST </span>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
