
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FileManagement.aspx.cs" Inherits="RealERPWEB.F_33_Doc.FileManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     


    <style>
        .listbox {
            margin-top: 1rem;
        }

        option {
            font-size: 1.2rem;
            padding: 0.4rem;
        }

        a {
            cursor: pointer;
        }

        .previcon {
            font-size: 0.5rem;
        }
    </style>
    <script type="text/javascript">
     $(document).ready(function () {
            console.log($("#<%= ddlPrjName.ClientID%>"));
            console.log($("#<%= txtFileName.ClientID%>"));
         isProjectCheck();

         $(document).on("click", "#isProject", function (e) {
             isProjectCheck();
            });
            //$("#isProject").change(function () {
               
             
            //});
        <%--  $("#<%= isProject.ClientID%>").click(function () {
                
                isProjectCheck();
            });--%>
          
        });
       function isProjectCheck() {
           //   console.log("I am iin");
             $("#<%= txtFileName.ClientID%>").Text="";
        if ($("#<%= isProject.ClientID%>").is(':checked')) {
                $("#<%= ddlPrjName.ClientID%>").show();
                $("#<%= txtFileName.ClientID%>").hide();
            }
            else {
                $("#<%= ddlPrjName.ClientID%>").hide();
                $("#<%= txtFileName.ClientID%>").show();
        }
             
        }

        function CloseModal() {
            $('#modalfolder').modal('hide');
            $('#modalfolderRename').modal('hide');
        }
        function ShowModal() {
            $('#modalfolderRename').modal('show');

        }

        function CloseModalFile() {
            $('#modalFile').modal('hide');
           

        }


        //function uploadComplete(sender) {

        //}

        //function uploadError(sender) {

        //}
      
    </script>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="card card-fluid mt-2">
        <div class="card-body">
            <div class="row">
                <div class="col-md-1">
                    <div class="form-group">
                        <asp:Label ID="lblPath" runat="server" CssClass="control-label">Path:</asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblFolderPath" runat="server" CssClass="form-control" Text="Root"></asp:Label>
                        <asp:Label ID="lblFolderPathtag" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-md-7 d-flex justify-content-end">
                    <div class="form-group">
                         <asp:LinkButton ID="LbtnHome" OnClick="LbtnHome_Click" runat="server" class="btn btn-warning " ToolTip="Home"><span class="fas fa-home"></span></asp:LinkButton>
                        <asp:LinkButton ID="LbtnBack" OnClick="LbtnBack_Click" runat="server" class="btn btn-warning " ToolTip="Back"><span class="fas fa-backward"></span></asp:LinkButton>
                        <a href="#" data-toggle="modal" onclick="isProjectCheck();" data-target="#modalfolder" class="btn btn-primary text-white" title="New Folder"><i><span class="fas fa-folder-plus"></span></i></a>
                        <a href="#" data-toggle="modal" data-target="#modalFile" runat="server" class="btn btn-dark text-white" title="New File Upload"><i><span class="fa fa-upload"></span></i>                          
                        </a>
                         <asp:LinkButton ID="lbtnDownload" CssClass="btn btn-secondary" runat="server" ToolTip="Download" OnClick="lbtnDownload_Click"><span class="menu-icon fas fa-download"></span></asp:LinkButton>
                     
                        <asp:LinkButton ID="lbtnDeleteFile" CssClass="btn btn-danger" runat="server" OnClientClick="return confirm('Do you Really want to Remove this file/Directory?')" ToolTip="Delete" OnClick="lbtnDeleteFile_Click"><i><span class="fa fa-trash-alt"></span></i>                                     
                        </asp:LinkButton>
                          <asp:LinkButton ID="LbtnRename" CssClass="btn btn-success" runat="server" ToolTip="Rename" OnClick="LbtnRename_Click"><span class="menu-icon fas fa-edit"></span></asp:LinkButton>
                          <asp:LinkButton ID="LbtnEmail" CssClass="btn btn-success" runat="server" ToolTip="Email" OnClick="LbtnEmail_Click"><span class="menu-icon fas fa-envelope-open"></span></asp:LinkButton>

                         <asp:LinkButton ID="lbtnForward" OnClick="lbtnForward_Click" runat="server" class="btn btn-warning " ToolTip="Forward"><span class="fas fa-forward"></span></asp:LinkButton>
                       <%-- <asp:LinkButton ID="lbtnDeleteFolder" CssClass="btn  btn-danger" runat="server" ToolTip="Delete Folder" OnClick="lbtnDeleteFolder_Click"><i><span class="previcon menu-icon fas fa-times"></span><span class="menu-icon fas fa-folder"></span></i>
                                    Delete Folder
                        </asp:LinkButton>--%>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card card-fluid">
        <div class="card-body">
            <div class="row">
                <div class="col-md-8">
                    <div class="row">
            <%--   <asp:ListBox ID="ListBox1" runat="server" Width="112px">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:ListBox>
        <asp:ListBox ID="ListBox2" runat="server" Width="111px"></asp:ListBox>--%>
                        <asp:ListView ID="ListView1" runat="server" Visible="false"  ItemPlaceholderID="itemplaceholder" >

                                 <LayoutTemplate>
                                  <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                 </LayoutTemplate>
                              <ItemTemplate>
                                   <div class="col-xs-12 col-sm-4 col-md-2 listDiv ">                  
                     
                                  <asp:LinkButton ID="LbtnFolder" runat="server" OnClientClick="BtnOnClick">                                  
                                        <img src="<%# ResolveUrl("~/Images/folder-icon-windows-10.jpg")%>"
                                            width="100" height="100" style="border: solid" />
                                      </asp:LinkButton>
                              
                                    <a href="#" class="small">   <%# Eval("tag")%>                                     
                                    </a>                            
                         
                                        </div>
                                       
                                  </ItemTemplate>
                        </asp:ListView>
                        </div>
                   <%--    <asp:ListView ID="productList" runat="server" 
                DataKeyNames="Uid" GroupItemCount="4"   
          SelectMethod="GetProducts">
                <EmptyDataTemplate>
                    <table >
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td/>
                </EmptyItemTemplate>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td runat="server">
                        <table>
                            <tr>
                                <td>
                                  <asp:LinkButton ID="LbtnFolder" runat="server"  OnClick="LbtnFolder_Click">Hello</asp:LinkButton>
                                   <%-- <a href="WorkDetails.aspx?WorkID=">
                                        <img src="#"
                                            width="100" height="75" style="border: solid" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="WorkDetails.aspx?WorkID">
                                        <span>
                                           hsgss
                                        </span>
                                    </a>
                                    <br />
<%--                                    <span>
                                        <a href="<%#:this.ResolveClientUrl(Item.URLPath)%>">Download</a>
                                    </span>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        </p>
                    </td>
                </ItemTemplate>
                <LayoutTemplate>
                    <table style="width:100%;">
                        <tbody>
                            <tr>
                                <td>
                                    <table id="groupPlaceholderContainer" runat="server" style="width:100%">
                                        <tr id="groupPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
            </asp:ListView>--%>
                        <asp:ListBox ID="lstFolder" runat="server"
                            BackColor="#DFF0D8" Font-Bold="True" Font-Size="12px" Height="400px" AutoPostBack="true"
                            TabIndex="12" CssClass="form-control listbox"></asp:ListBox>
                   
                </div>

                <div class="col-md-4 text-center" id="PanelView" visible="false" runat="server">
                  <%-- <asp:Literal ID="ltEmbed" runat="server" />--%>
                     <asp:Literal ID="embedPdf" runat="server"/>
                    <asp:Image ID="BindImg" runat="server" CssClass="img img-thumbnail" /><br />
                  <table class="table-striped table-hover table-bordered grvContentarea" id="tbldetails" runat="server" style="width:100%">
                      <tr>
                          <td>File Name</td>
                          <td><asp:Label ID="TblFileName" runat="server"></asp:Label> </td>                         
                      </tr>
                       <tr>
                          <td>File Size</td>
                        <td><asp:Label ID="TblFilesize" runat="server"></asp:Label> </td>                
                      </tr>
                       <tr>
                          <td>File Type</td>
                          <td><asp:Label ID="TblExtension" runat="server"></asp:Label> </td>                   
                      </tr>
                        <tr>
                          <td>Last Modification</td>
                         <td><asp:Label ID="lastmodified" runat="server"></asp:Label> </td>                      
                      </tr>
                  </table>
                     <asp:HyperLink ID="HypLInk" runat="server" CssClass="btn btn-xs btn-success" Target="_blank">Download</asp:HyperLink>
                </div>
            </div>
        </div>
    </div>



    <div class="modal fade" id="modalfolder" role="dialog">
        <div class="modal-dialog  modal-lg ">
            <div class="modal-content ">

                <div class="modal-body">

                    <div class="form-group">
                        <label class="control-label">Folder Name:</label>
                        <div class="proj-container">
                            <asp:CheckBox runat="server" ID="isProject" Text="Project" ClientIDMode="Static"/>
                           
                        </div>
                        <asp:TextBox ID="txtFileName" runat="server" CssClass=" form-control"></asp:TextBox>
                        <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="form-control inputTxt">
                        </asp:DropDownList>

                    </div>

                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="lbtnCreateFolder" CssClass="btn btn-primary btn-sm" runat="server" OnClientClick="CloseModal();" ToolTip="Save" OnClick="lbtnCreateFolder_Click"><span class=" glyphicon  glyphicon-saved"></span>Save</asp:LinkButton>
                  
                    <button class="btn btn-primary btn-sm" data-dismiss="modal" aria-hidden="true">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalfolderRename" role="dialog">
        <div class="modal-dialog  modal-lg ">
            <div class="modal-content ">

                <div class="modal-body">

                    <div class="form-group">
                        <label class="control-label">Folder Name:</label>                       
                        <asp:TextBox ID="TxtRenameFolder" runat="server" CssClass=" form-control"></asp:TextBox>                     

                    </div>

                </div>
                <div class="modal-footer">
                  
                    <asp:LinkButton ID="lbtnRenameFolder" CssClass="btn btn-primary btn-sm" OnClientClick="CloseModal();" runat="server" ToolTip="Rename Folder" OnClick="lbtnRenameFolder_Click">Rename</asp:LinkButton>
                    <button class="btn btn-primary btn-sm" data-dismiss="modal" aria-hidden="true">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalFile" role="dialog">
        <div class="modal-dialog  modal-sm ">
            <div class="modal-content ">

                <div class="modal-body">
                    <div class="form-group">          
                        <cc1:AsyncFileUpload
                            runat="server"
                            ID="AsyncFileUpload1" Visible="false" UploaderStyle="Traditional" ClientIDMode="Static" OnUploadedComplete="FileUploadComplete"
                            ThrobberID="imgLoader" />
                        <br />
                         <div id="dropzone" class="fileinput-dropzone">
                          <span>Drop files or click to upload.</span> <!-- The file input field used as target for the file upload widget -->
                          <asp:FileUpload ID="FileUpload" runat="server" 
                                                     />
                          
                        </div>
                       
                        <%--<asp:FileUpload id="AsyncFileUpload1" runat="server" />--%>
                    </div>

                </div>
                <div class="modal-footer">


                    <asp:LinkButton ID="LbtnUpload" Visible="false" OnClick="LbtnUpload_Click"  runat="server">Upload</asp:LinkButton>
                    <asp:LinkButton ID="lbtnCreateFile" CssClass="btn btn-primary btn-xs" runat="server" OnClientClick="CloseModalFile();" ToolTip="File Upload" OnClick="lbtnCreateFile_Click"><span class="glyphicon glyphicon-saved"></span>Save</asp:LinkButton>



                    <%--<asp:LinkButton ID="lbtnCreateFolder" CssClass="btn btn-primary btn-xs" runat="server" ToolTip="New Folder" OnClick="lbtnCreateFolder_Click"><i><span class=" glyphicon  glyphicon-plus"><span class=" glyphicon  glyphicon-folder-open "></i></span></asp:LinkButton>--%>
                    <button class="btn btn-primary btn-sm" data-dismiss="modal" aria-hidden="true">Close</button>
                </div>
            </div>
        </div>
    </div>

 
    


            </ContentTemplate>

        <Triggers>

                <asp:PostBackTrigger ControlID="lbtnCreateFile" />

            </Triggers>
       
    </asp:UpdatePanel>
</asp:Content>

