<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccSubCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccSubCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Content/FullGridPager.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function loadModal() {
            $('#detialsinfo').modal('toggle');
        }

        function CloseModal() {
            $('#detialsinfo').modal('hide');

             
        }


        function loadModalAddCode() {
            $('#AddResCode').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }




        function CloseModalAddCode() {
            $('#AddResCode').modal('hide');


        }

        function pageLoaded() {

            $('#Chboxchild').change(function () {
                var result = $('#Chboxchild').is(':checked');
                var description = result ? "Add Child" : "Add Group";
                $('#lblchild').html(description);


            });

            $('.chzn-select').chosen({ search_contains: true });



        }

        function IsNumberWithOneDecimal(txt, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8)) {
                return false;
            } else {
                var len = txt.value.length;
                var index = txt.value.indexOf('.');
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    if ((len + 1) - index > 3) {
                        return false;
                    }
                }

            }
            return true;
        }

    </script>


     <style>
         .switch {
             position: relative;
             display: inline-block;
             width: 40px;
             height: 20px;
         }

             .switch input {
                 opacity: 0;
                 width: 0;
                 height: 0;
             }

         .slider {
             position: absolute;
             cursor: pointer;
             top: 0;
             left: 0;
             right: 0;
             bottom: 0;
             background-color: #ccc;
             -webkit-transition: .4s;
             transition: .4s;
         }

             .slider:before {
                 position: absolute;
                 content: "";
                 height: 18px;
                 width: 18px;
                 left: 1px;
                 bottom: 1px;
                 background-color: white;
                 -webkit-transition: .4s;
                 transition: .4s;
             }

         input:checked + .slider {
             background-color: #2196F3;
         }

         input:focus + .slider {
             box-shadow: 0 0 1px #2196F3;
         }

         input:checked + .slider:before {
             -webkit-transform: translateX(18px);
             -ms-transform: translateX(18px);
             transform: translateX(18px);
         }

         /* Rounded sliders */
         .slider.round {
             border-radius: 20px;
         }

             .slider.round:before {
                 border-radius: 50%;
             }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">                       
                         <fieldset class="scheduler-border">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <asp:Label ID="LblBookName1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Select Code Book:"></asp:Label>



                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control inputTxt chzn-select" OnSelectedIndexChanged="ddlOthersBook_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>

                                        <%--                                                <cc1:ListSearchExtender ID="ddlCodeBook_ListSearchExtender" runat="server"
                                                    Enabled="True" QueryPattern="Contains" TargetControlID="ddlCodeBook">
                                                </cc1:ListSearchExtender>--%>

                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Value="2">Main Code</asp:ListItem>
                                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                            <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">

                                     <div ="col-md-2 control-label lblTxt">
                                           <asp:Label ID="lblcatagory" runat="server"  CssClass="col-md-2 control-label lblTxt" Text="Catagory"></asp:Label>

                                    </div>


                                     <div class="col-md-3 pading5px  asitCol3">
                                        
                                          <asp:DropDownList ID="ddlcatagory" runat="server" CssClass=" chzn-select form-control">
                                        </asp:DropDownList>

                                    </div>

                                  
                                    <div class="col-md-2 pading5px asitCol2">
                                       <asp:Label ID="LblBookName2" runat="server" CssClass=" smLbl_to" Text="Search Option:"></asp:Label>
                                        <asp:TextBox ID="txtsrch" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success srearchBtn" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>


                                    </div>


                                    



                                    <div class="col-md-2 pading5px">

                                        <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page Size" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                               
                                </div>

                            </div>
            
                    </fieldset>

                    </div>
                        <div class="table-responsive">
                        

                            
                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" PageSize="15" OnPageIndexChanging="grvacc_PageIndexChanging"
                                OnRowDataBound="grvacc_RowDataBound" CssClass="table table-striped table-hover table-bordered grvContentarea" Width="700px" OnDataBound="grvacc_DataBound">
                            
                               
                                <FooterStyle BackColor="#5F9467" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="+">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent" Visible="false" OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                        
                                        <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />
                                    
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                     <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" HeaderStyle-Width="50px"
                                    SelectText="" ShowEditButton="True"     EditText="&lt;i class=&quot;fa fa-pencil-square-o&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                    <HeaderStyle />
                                    <ItemStyle ForeColor="#0000C0"/>
                                </asp:CommandField>

                                   



                                    <asp:TemplateField HeaderText=" ">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbllgrcode" runat="server" Font-Size="12px" Height="16px"
                                                MaxLength="13" BackColor="Transparent"
                                                
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                Width="100px"></asp:Label>


                                          
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="80px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Description of Code" HeaderStyle-Width="400px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="250"
                                                Style="border-style: none;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="350px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="350px"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                             
                                            <asp:HyperLink ID="hlnkgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                                Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))  %>'
                                                Width="350px">                                             
                                            
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Description of Code BN" HeaderStyle-Width="400px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDescbn" runat="server" Font-Size="12px" MaxLength="250"
                                                Style="border-style: none;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdescbn")) %>'
                                                Width="300px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label8" runat="server" Text="Description of Code BN" Width="300px"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdescbn"))  %>' Width="300px"></asp:Label>
                                             
                                             
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirunit" runat="server" MaxLength="100" Visible="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="40px"></asp:TextBox>

                                            <asp:DropDownList ID="ddlUnit" CssClass="chzn-select form-control" Visible="false" runat="server">
                                            </asp:DropDownList>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Std.Rate">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-style: none;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsirval" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgridsirtype" runat="server" Font-Size="12px" MaxLength="10"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                Width="60px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltype" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brand" HeaderStyle-Width="200">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                Width="200px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltypedesc" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Department" Visible="false">
                                        <EditItemTemplate>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <table style="width: 100%;">
                                                    <tr>

                                                        <td>
                                                            <asp:LinkButton ID="ibtnSrchProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlProName" runat="server" CssClass=" ddlPage" Style="width: 200px;" TabIndex="6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProNames" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Details">

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDetails" runat="server" CssClass="btn btn-sm  btn-primary" Visible="false" OnClick="lbtnDetails_Click">Details</asp:LinkButton>

                                            <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode3")) %>'
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entry User Name" Visible="false">
                                        <EditItemTemplate>
                                            <asp:Label ID="tlblgvUsr" runat="server" Font-Size="12px" MaxLength="100"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                                Width="90px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>



                           <PagerTemplate>
                    <table id="pagerOuterTable" class="pagerOuterTable"  runat="server" >
                        <tr>
                            <td>
                                <table id="pagerInnerTable" cellpadding="2"  cellspacing="1" runat="server">
                                    <tr>
                                        <td class="pageCounter">
                                            <asp:Label ID="lblPageCounter"  ClientIDMode="Static" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="pageFirstLast">
                                            <img src="../Image/firstpage.gif" align="absmiddle" />&nbsp;<asp:LinkButton ID="lnkFirstPage" ClientIDMode="Static" CssClass="pagerLink" runat="server" CommandName="Page" CommandArgument="First">First</asp:LinkButton>
                                        </td>
                                        <td class="pagePrevNextNumber">
                                            <asp:ImageButton ID="imgPrevPage"  ClientIDMode="Static" runat="server" ImageAlign="AbsMiddle" ImageUrl="../Image/prevpage.gif" CommandName="Page" CommandArgument="Prev" />
                                        </td>

                                        <td class="pagePrevNextNumber">
                                            <asp:ImageButton ID="imgNextPage" ClientIDMode="Static"  runat="server" ImageAlign="AbsMiddle" ImageUrl="../Image/nextpage.gif" CommandName="Page" CommandArgument="Next" />
                                        </td>
                                        <td class="pageFirstLast">
                                            <asp:LinkButton ID="lnkLastPage" CssClass="pagerLink"  ClientIDMode="Static" CommandName="Page" CommandArgument="Last" runat="server">Last</asp:LinkButton>&nbsp;<img src="../Image/lastpage.gif" align="absmiddle" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td visible="false" class="pageGroups">Pages:&nbsp;<asp:DropDownList ID="ddlPageGroups" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageGroups_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </PagerTemplate>

                                  


                                <RowStyle />
                                <EditRowStyle />
                                <SelectedRowStyle />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <%--<PagerStyle CssClass="gvPagination" />--%>
                                <HeaderStyle CssClass="grvHeader" />
                                <AlternatingRowStyle BackColor="" />
                            </asp:GridView>



                        </div>
                
                </div>
               
            </div>

                <div class="modal fade " id="detialsinfo" role="dialog">
                    <div class="modal-dialog  modal-lg ">
                        <div class="modal-content ">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title text-center">Details Information</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label class="control-label">Details:</label>
                                    <asp:TextBox ID="txtrsircode" runat="server" TextMode="MultiLine" CssClass="form-control" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                                </div>

                               <%-- <div class="form-group">
                                    <label class="control-label">Catagory:</label>
                                  
                                    <asp:DropDownList ID="ddlworkCatagory" runat="server" CssClass="form-control">

                                        <asp:ListItem Value="">Standard</asp:ListItem>
                                        <asp:ListItem Value="A">Appartment</asp:ListItem>
                                        <asp:ListItem Value="O">Office</asp:ListItem>

                                    </asp:DropDownList>

                                </div>--%>


                            </div>
                            <div class="modal-footer">
                                <%--    <button class="btn btn-success" data-dismiss="modal" aria-hidden="true" >Update</button>--%>
                                <asp:LinkButton ID="lbtnUpdateDetails" runat="server" class="btn btn-success" aria-hidden="true" OnClientClick="CloseModal();" OnClick="lbtnUpdateDetails_Click">Update</asp:LinkButton>
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                    </div>
                </div>



            

    <div id="AddResCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content  ">
                <div class="modal-header">

                    <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                    <h4 class="modal-title">
                        <span class="fa fa-table"></span> Add New Code  </h4>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="row-fluid">
                        <asp:Label ID="lblsircode" runat="server" Visible="false"></asp:Label>

                         <div class="form-group" runat="server">
                            <label class="col-md-4">Resource Code </label>


                            <div class="col-md-8">
                                <asp:TextBox ID="txtresourcecode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" runat="server">
                            <label class="col-md-4">Description EN</label>


                            <div class="col-md-8">
                                <asp:TextBox ID="txtresourcehead" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                         <div class="form-group" runat="server">
                            <label class="col-md-4">Description BN</label>


                            <div class="col-md-8">
                                <asp:TextBox ID="txtresourceheadBN" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-md-4">Unit </label>
                            <div class="col-md-5">
                                <asp:TextBox ID="txtunit" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:DropDownList ID="ddlUnits" CssClass="chzn-select form-control" Visible="false" runat="server">
                                            </asp:DropDownList>


                            </div>




                            <div class="col-md-3">
                                <label id="chkbod" runat="server" class="switch">
                                    <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" />
                                    <span class="btn btn-xs slider round"></span>
                                </label>
                                <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4" id="lblsdrate" runat="server">Standard Rate </label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtstdrate" runat="server" CssClass="form-control" onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Brand </label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtbrand" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                       


                        <div class="form-group">
                            <label id="lblddlproject" runat="server" class="col-md-4">Department</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>


                    </div>


                </div>
                <div class="modal-footer ">
                    <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>


                    <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                </div>
            </div>
        </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


